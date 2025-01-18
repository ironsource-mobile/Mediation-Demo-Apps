#import "LPMUtilities.h"

@implementation LPMUtilities

+ (NSString *)getStringFromCString:(const char *)_x_ {
    return (_x_ != NULL) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""];
}

+ (NSString *)serializeAdInfoToJSON:(LPMAdInfo *)adInfo {
    NSDictionary *adInfoDict = @{
        @"adUnitId": adInfo.adUnitId ?: @"",
        @"adUnitName": adInfo.adUnitName ?: @"",
        @"adSize": [self serializeAdSizeToJSON:adInfo.adSize],
        @"adFormat": adInfo.adFormat ?: @"",
        @"placementName": adInfo.placementName ?: @"",
        @"auctionId": adInfo.auctionId ?: @"",
        @"country": adInfo.country ?: @"",
        @"ab": adInfo.ab ?: @"",
        @"segmentName": adInfo.segmentName ?: @"",
        @"adNetwork": adInfo.adNetwork ?: @"",
        @"instanceName": adInfo.instanceName ?: @"",
        @"instanceId": adInfo.instanceId ?: @"",
        @"revenue": adInfo.revenue ?: @"",
        @"precision": adInfo.precision ?: @"",
        @"encryptedCPM": adInfo.encryptedCPM ?: @""
    };
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:adInfoDict options:0 error:&error];
    return jsonData ? [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] : @"";
}

+ (NSString *)serializeErrorToJSON:(NSError *)adError{
    NSLog(@"levelplay failed to load-3");
    NSDictionary *errorDict = @{
        @"errorCode": [@(adError.code) stringValue] ?: @"",
        @"errorMessage": adError.description ?: @""
    };
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:errorDict options:0 error:&error];
    return jsonData ? [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] : @"";
}

+ (NSString *)serializeErrorToJSON:(NSError *)adError adUnitId:(NSString *)adUnitId {
    NSDictionary *errorDict = @{
        @"errorCode": [@(adError.code) stringValue] ?: @"",
        @"errorMessage": adError.description ?: @"",
        @"adUnitId": adUnitId ?: @""
    };
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:errorDict options:0 error:&error];
    return jsonData ? [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] : @"";
}

#pragma mark - private methods

+ (NSString *)serializeAdSizeToJSON:(LPMAdSize *)adSize {
    if (adSize == nil) {
        return @"";
    }
    NSDictionary *adSizeDict = @{
        @"description": adSize.sizeDescription ?: @"",
        @"width": @(adSize.width) ?: @0,
        @"height": @(adSize.height) ?: @0
    };
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:adSizeDict options:0 error:&error];
    return jsonData ? [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding] : @"";
}


@end
