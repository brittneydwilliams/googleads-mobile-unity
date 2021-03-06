﻿// Copyright 2018 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if UNITY_IOS

using System;
using UnityEngine;

using GoogleMobileAds.Api.Mediation.Vungle;
using GoogleMobileAds.Common.Mediation.Vungle;

namespace GoogleMobileAds.iOS.Mediation.Vungle
{
    public class VungleClient : IVungleClient
    {
        private static VungleClient instance = new VungleClient();
        private VungleClient() {}

        public static VungleClient Instance
        {
            get
            {
                return instance;
            }
        }

        public void UpdateConsentStatus(VungleConsent consentStatus,
                                        String consentMessageVersion)
        {
            if (consentStatus == VungleConsent.UNKNOWN) {
                MonoBehaviour.print ("Cannot call '[VungleRouterConsent updateConsentStatus:]' with unknown consent status.");
                return;
            }

            string parameterString = "";
            if (consentStatus == VungleConsent.ACCEPTED) {
                parameterString = "VungleConsentAccepted";
            } else if (consentStatus == VungleConsent.DENIED) {
                parameterString = "VungleConsentDenied";
            }
            parameterString += ", '" + consentMessageVersion + "'";

            MonoBehaviour.print ("Calling '[VungleRouterConsent updateConsentStatus:]' with arguments: " + parameterString);
            Externs.GADUMUpdateConsentStatus( (int)consentStatus, consentMessageVersion );
        }

        public VungleConsent GetCurrentConsentStatus()
        {
            return (VungleConsent)Externs.GADUMGetCurrentConsentStatus();
        }

        public String GetCurrentConsentMessageVersion()
        {
            return Externs.GADUMGetCurrentConsentMessageVersion();
        }
    }
}

#endif
